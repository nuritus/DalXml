namespace Dal;
using DalApi;
using System.Xml.Linq;


///////////////////////////////////////
//implement IStudent with linq to XML
///////////////////////////////////////
class Student : IStudent
{
    const string s_students = "students"; //Linq to XML

    static DO.Student? getStudent(XElement s) =>
        s.ToIntNullable("ID") is null ? null : new DO.Student()
        {
            ID = (int)s.Element("ID")!,
            FirstName = (string?)s.Element("FirstName"),
            LastName = (string?)s.Element("LastName"),
            StudentStatus = s.ToEnumNullable<DO.StudentStatus>("StudentStatus"),
            BirthDate = s.ToDateTimeNullable("BirthDate"),
            Grade = s.ToDoubleNullable("Grade"),
        };

    static IEnumerable<XElement> createStudentElement(DO.Student student)
    {
        yield return new XElement("ID", student.ID);
        if (student.FirstName is not null)
            yield return new XElement("FirstName", student.FirstName);
        if (student.LastName is not null)
            yield return new XElement("LastName", student.LastName);
        if (student.StudentStatus is not null)
            yield return new XElement("StudentStatus", student.StudentStatus);
        if (student.BirthDate is not null)
            yield return new XElement("BirthDate", student.BirthDate);
        if (student.LastName is not null)
            yield return new XElement("Grade", student.Grade);
    }

    public IEnumerable<DO.Student?> GetAll(Func<DO.Student?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_students).Elements().Select(s => getStudent(s))
        : XMLTools.LoadListFromXMLElement(s_students).Elements().Select(s => getStudent(s)).Where(filter);

    public DO.Student GetById(int id) =>
        (DO.Student)getStudent(XMLTools.LoadListFromXMLElement(s_students)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new Exception("missing id"))!;

    public int Add(DO.Student student)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        if (XMLTools.LoadListFromXMLElement(s_students)?.Elements()
            .FirstOrDefault(st => st.ToIntNullable("ID") == student.ID) is not null)
            // fix to: throw new DalMissingIdException(id);;
            throw new Exception("id already exist");

        studentsRootElem.Add(new XElement("Student", createStudentElement(student)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);

        return student.ID; ;
    }

    public void Delete(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_students);

        (studentsRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int?)st.Element("ID") == id) ?? throw new Exception("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_students);
    }

    public void Update(DO.Student doStudent)
    {
        Delete(doStudent.ID);
        Add(doStudent);
    }
}
