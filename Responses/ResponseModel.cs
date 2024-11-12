namespace iserra_api.Responses {
    public class ResponseModel<T> {
        public T? Dados { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
