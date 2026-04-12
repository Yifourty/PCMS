using PCMS.Application.Common.Dtos;

namespace PCMS.Application.Common.Validators
{
    public static class ProductValidator
    {
        public static bool IsValid(CreateProductDto dto)
        {
            return dto switch
            {
                { Name: null or "" } => false,
                { Price: <= 0 } => false,
                { Quantity: < 0 } => false,
                _ => true
            };
        }
    }
}
