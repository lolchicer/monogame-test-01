namespace MonogameTest01;

// так я и не понял по какой причине нельзя
// из-за невозможности скастить IAngle во float я не могу сделать реализации IAngle структурами
// касты выполняют роль этого интерфейса
public interface IAngle
{
    public float Value { get; set; }
}
