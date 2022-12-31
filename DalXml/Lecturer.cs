namespace Dal;

///////////////////////////////////////////
//implement ILecturer with XML Serializer
//////////////////////////////////////////
class Lecturer : DalApi.ILecturer
{
    const string s_lecturers = "lecturers"; //XML Serializer

    public IEnumerable<DO.Lecturer?> GetAll(Func<DO.Lecturer?, bool>? filter = null)
    {
        var listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers)!;
        return filter == null ? listLecturers.OrderBy(lec => ((DO.Lecturer)lec!).ID)
                              : listLecturers.Where(filter).OrderBy(lec => ((DO.Lecturer)lec!).ID);
    }

    public DO.Lecturer GetById(int id) =>
        XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers).FirstOrDefault(p => p?.ID == id)
        //DalMissingIdException(id, "Lecturer");
        ?? throw new Exception("missing id");

    public int Add(DO.Lecturer lecturer)
    {
        var listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers);

        if (listLecturers.Exists(lec => lec?.ID == lecturer.ID))
            throw new Exception("id already exist");//DalAlreadyExistIdException(lecturer.ID, "Lecturer");

        listLecturers.Add(lecturer);

        XMLTools.SaveListToXMLSerializer(listLecturers, s_lecturers);

        return lecturer.ID;
    }

    public void Delete(int id)
    {
        var listLecturers = XMLTools.LoadListFromXMLSerializer<DO.Lecturer>(s_lecturers);

        if (listLecturers.RemoveAll(p => p?.ID == id) == 0)
            throw new Exception("missing id"); //new DalMissingIdException(id, "Lecturer");

        XMLTools.SaveListToXMLSerializer(listLecturers, s_lecturers);
    }

    public void Update(DO.Lecturer lecturer)
    {
        Delete(lecturer.ID);
        Add(lecturer);
    }
}
