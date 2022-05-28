namespace PPPK_DZ2.Models
{
    public class Student : Person
    {
        public int Ects { get; set; }

        public override string ToString()
            => base.ToString() + ", " + Ects;
    }
}
