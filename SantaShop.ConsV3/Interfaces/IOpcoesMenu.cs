namespace SantaShop.ConsV3.Interfaces
{
    public interface IOpcoesMenu
    {
        public void ListarTodos(bool soLista=false);
        public void ProcurarPorPesquisa();
        public void Adicionar();
        public void Actualizar();
        public void Eliminar();

    }
}
