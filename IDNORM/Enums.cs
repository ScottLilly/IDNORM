namespace IDNORM
{
    public enum Comparators
    {
        EqualTo,
        NotEqualTo,
        LessThan,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,
        StartsWith,
        EndsWith,
        Contains
    }

    public enum DbType
    {
        Mock,
        MSSQL2008,
        PostgreSQL
    }

}
