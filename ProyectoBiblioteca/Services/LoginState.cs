using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoBiblioteca.Services;

public class LoginState
{
    private int? _usuarioActual = null;
    public int? UsuarioActual
    {
        get => _usuarioActual;
        private set { 
            if (_usuarioActual == value) return;
            _usuarioActual = value;
            OnChange?.Invoke();
        }
    }

    public event Action? OnChange;
    public void SetValor(int? nuevo) => UsuarioActual = nuevo;
}