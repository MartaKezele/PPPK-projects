namespace PPPK_DZ2.Models
{
    public class Course
    {
        public int IDCourse { get; set; }
        public string CourseName { get; set; }
        public int Ects { get; set; }

        public override string ToString()
            => IDCourse + ", " + CourseName + ", " + Ects;

        public override bool Equals(object obj)
            => obj is Course other ? IDCourse == other.IDCourse && CourseName == other.CourseName && Ects == other.Ects : false;

        public override int GetHashCode()
            => IDCourse.GetHashCode() + CourseName.GetHashCode() + Ects.GetHashCode();
    }
}
