namespace Domain
{
    public class GdiaPersonalEspecialidad
    {
            public int GdiaPersonalEspecialidadId { get; set; }
            public string Descripcion { get; set; }
            public int CmnEstructuraId { get; set; }
            public bool MedicoVisita { get; set; }
            public GdiaPersonal? GdiaPersonal { get; set; }
    }
}
