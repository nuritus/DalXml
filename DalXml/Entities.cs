namespace DO;

public enum StudentStatus { ACTIVE, SUSPENDED, ACADEMIC_VACATION, FINISHED, LEFT }
public enum LecturerStatus { STUFF, SABBATICAL, ADJUNCT, PENSIONER, FIRED, LEFT }

public struct Student
{
    public int ID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public StudentStatus? StudentStatus { get; set; }
    public DateTime? BirthDate { get; set; }
    public double? Grade { get; set; }

    public override string ToString() => this.ToStringProperty();
}

public struct Lecturer
{
    public int ID { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public LecturerStatus? LecturerStatus { get; set; }
    public bool SeniorStuff { get; set; }

    public override string ToString() => this.ToStringProperty();
}
