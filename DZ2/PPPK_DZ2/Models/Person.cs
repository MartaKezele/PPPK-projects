using PPPK_DZ2.Utils;
using System.Windows.Media.Imaging;

namespace PPPK_DZ2.Models
{
    public abstract class Person
    {
        public int IDPerson { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Picture { get; set; }
        public BitmapImage Image => ImageUtils.ByteArrayToBitmapImage(Picture);

        public override string ToString()
            => IDPerson + ", " + FirstName + " " + LastName + ", " + Email;

        public override bool Equals(object obj)
            => obj is Person other ? IDPerson == other.IDPerson : false;

        public override int GetHashCode()
            => IDPerson.GetHashCode();
    }
}
