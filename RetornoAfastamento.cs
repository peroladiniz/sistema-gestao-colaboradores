namespace API_SIC_WEB_ANGULAR.Model
{
    public class RetornoAfastamento
    {
        public string Matricula { get; set; }
        public string Matricula_Log { get; set; }
        public DateTime DtIni { get; set; }
        public DateTime? DtFim { get; set; }
        public int Saida_Retorno { get; set; }
    }
}
