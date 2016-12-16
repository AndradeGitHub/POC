using System;

namespace eventos.domain
{
    public delegate void VelocidadeSegurancaExcedidaEventHandler(object source, EventArgs e);

    public class Carro
    {
        public event VelocidadeSegurancaExcedidaEventHandler ExcedeuVelocidadeSeguranca;

        private int _velocidade = 0;
        private int _velocidadeSeguranca = 70;

        public int Velocidade
        {
            get
            {
                return _velocidade;
            }
        }

        public void Acelerar(int kmh)
        {
            int velocidadeAnterior = _velocidade;
            _velocidade += kmh;
            
            if (velocidadeAnterior <= _velocidadeSeguranca && _velocidade > _velocidadeSeguranca)            
                OnVelocidadeSegurancaExcedida(new EventArgs());            
        }

        public virtual void OnVelocidadeSegurancaExcedida(EventArgs e)
        {
            if (ExcedeuVelocidadeSeguranca != null)
                ExcedeuVelocidadeSeguranca(this, e);
        }
    }
}
