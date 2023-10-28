namespace ControleFacil.Api.Contract.Apagar
{
    public class ApagarRequestContract : TituloRequestContact
    {
        public double ValorPago { get; set; }

        public DateTime? DataPagamento { get; set; }
    }
}