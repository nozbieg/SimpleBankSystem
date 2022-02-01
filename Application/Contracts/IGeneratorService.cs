using System;
using System.Linq;

namespace Application.Contracts;

public interface IGeneratorService
{
    string GenerateCardNumber();
}
