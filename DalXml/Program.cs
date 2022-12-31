Test_Student_LinqToXml();
Test_Lecturer_XmlSerializer();

static void Test_Student_LinqToXml()
{
    DalApi.IStudent dal = new Dal.Student();
    try
    {
        for (int i = 10; i > 0; --i)
            dal.Add(new()
            {
                ID = i,
                FirstName = "FN" + i,
                LastName = "LN" + i,
                StudentStatus = DO.StudentStatus.ACTIVE,
                BirthDate = DateTime.ParseExact("12.03.85", "dd.MM.yy", null),
                Grade = 100
            });

        Console.WriteLine(dal.GetById(1));
        dal.Delete(5);
        dal.Update(new DO.Student
        {
            ID = 3,
            FirstName = "FNNew",
            //LastName = "LNNew",
            //StudentStatus = DO.StudentStatus.FINISHED,
            BirthDate = DateTime.ParseExact("15.05.55", "dd.MM.yy", null),
            Grade = 100
        });

        foreach (var item in dal.GetAll()) Console.WriteLine(item);
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}

static void Test_Lecturer_XmlSerializer()
{
    DalApi.ILecturer dal = new Dal.Lecturer();
    try
    {
        for (int i = 10; i > 0; --i)
            dal.Add(new()
            {
                ID = i,
                FirstName = "FN" + i,
                LastName = "LN" + i,
                LecturerStatus = DO.LecturerStatus.SABBATICAL,
                SeniorStuff = true
            });

        Console.WriteLine(dal.GetById(1));
        dal.Delete(5);
        dal.Update(new DO.Lecturer
        {
            ID = 3,
            FirstName = "FN3",
            //LastName = "LN3",
            //LecturerStatus = DO.LecturerStatus.ADJUNCT,
            SeniorStuff = false
        });

        foreach (var item in dal.GetAll()) Console.WriteLine(item);
    }
    catch (Exception ex) { Console.WriteLine($"{ex.Message}\n{ex.InnerException}"); }
}
