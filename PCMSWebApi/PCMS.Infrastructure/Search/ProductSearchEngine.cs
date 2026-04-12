namespace PCMS.Infrastructure.Search
{
    public class ProductSearchEngine<T>
    {
        public IEnumerable<T> Search(
            IEnumerable<T> items,
            string query,
            Func<T, string>[] fields)
        {
            return items
                .Select(item => new
                {
                    Item = item,
                    Score = CalculateScore(item, query, fields)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Item);
        }

        private int CalculateScore(
            T item,
            string query,
            Func<T, string>[] fields)
        {
            int score = 0;

            foreach (var field in fields)
            {
                var value = field(item);

                if (value.Contains(query, StringComparison.OrdinalIgnoreCase))
                    score += 10;

                score += FuzzyMatch(value, query);
            }

            return score;
        }

        private int FuzzyMatch(string source, string target)
        {
            int distance = Levenshtein(source, target);
            return Math.Max(0, 10 - distance);
        }

        private int Levenshtein(string s, string t)
        {
            var dp = new int[s.Length + 1, t.Length + 1];

            for (int i = 0; i <= s.Length; i++) dp[i, 0] = i;
            for (int j = 0; j <= t.Length; j++) dp[0, j] = j;

            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    int cost = s[i - 1] == t[j - 1] ? 0 : 1;

                    dp[i, j] = Math.Min(Math.Min(
                        dp[i - 1, j] + 1,
                        dp[i, j - 1] + 1),
                        dp[i - 1, j - 1] + cost);
                }
            }

            return dp[s.Length, t.Length];
        }
    }
}
