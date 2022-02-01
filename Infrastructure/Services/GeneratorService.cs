using System;
using System.Linq;
using Application.Contracts;
using Infrastructure.Extensions;
namespace Infrastructure.Services
{
    public class GeneratorService : IGeneratorService
    {
        public string GenerateCardNumber()
        {

            string generateAccId = $"400000{new Random().Next(0, 999999999):D9}";

            var generatedIdWithChecksum = generateAccId.AppendCheckDigit();


            return generatedIdWithChecksum;
        }
    }
}
