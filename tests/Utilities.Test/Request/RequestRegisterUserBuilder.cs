using Bogus;
using Bogus.Extensions.Brazil;
using Ecocell.Communication.Request.UserApp;

namespace Utilities.Test.Request;

public class RequestRegisterUserBuilder
{
    public static RequestRegisterUser NaturalPersonBuilder(int passwordLength = 8)
    {                
        return new Faker<RequestRegisterUser>()
            .RuleFor(c => c.Name, fake => fake.Person.FullName)
            .RuleFor(c => c.Email, fake => fake.Internet.Email())
            .RuleFor(c => c.Password, fake => fake.Internet.Password(passwordLength))
            .RuleFor(c => c.Document, fake => fake.Person.Cpf().Replace(".", "").Replace("-", ""))
            .RuleFor(c => c.BirthDate, fake => fake.Person.DateOfBirth)
            .RuleFor(c => c.Type, fake => 'N')
            .RuleFor(c => c.Cellphone, fake => fake.Phone.PhoneNumber("## 9 ########"));
    }

    public static RequestRegisterUser LegalPersonBuilder(int passwordLength = 8)
    {
        return new Faker<RequestRegisterUser>()
            .RuleFor(c => c.Name, fake => fake.Company.CompanyName())
            .RuleFor(c => c.CompanyName, fake => fake.Company.CompanyName())
            .RuleFor(c => c.Email, fake => fake.Internet.Email())
            .RuleFor(c => c.Password, fake => fake.Internet.Password(passwordLength))
            .RuleFor(c => c.Document, fake => fake.Company.Cnpj().Replace(".", "").Replace("-", "").Replace("/", ""))         
            .RuleFor(c => c.Type, fake => 'L') 
            .RuleFor(c => c.Cellphone, fake => fake.Phone.PhoneNumber("## 9 ########"));
    }
} 