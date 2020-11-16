using Hotel.Domain.Exceptions;
using Hotel.Domain.Extensions;

namespace Hotel.Domain.Validators
{
    public class AreaValidators
    {
        public static void ValidIfNameExist(string name)
        {
            if (name.IsNotExist())
                throw new MissingValueException("Nazwa obszaru jest wymagana.");
        }
    }
}
