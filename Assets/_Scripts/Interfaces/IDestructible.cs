public interface IDestructible
{
    public void Destruct();
    public void ShowDestructionUI(bool _show);
    public void Damage(float _amount);
    public bool CanDestruct(Item _usingItem);
}
