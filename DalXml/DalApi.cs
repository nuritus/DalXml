namespace DalApi;
using DO;

public interface ICrud<T> where T : struct
{
    int Add(T item);
    T GetById(int id);
    void Update(T item);
    void Delete(int id);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter = null); //stage 2
}

public interface IStudent : ICrud<Student> { } //Linq to XML
public interface ILecturer : ICrud<Lecturer> { } // XML Serializer
