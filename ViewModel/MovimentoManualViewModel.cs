﻿using SinqiaParibas45.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SinqiaParibas45.ViewModels
{
    public class MovimentoManualViewModel
    {
    
        // Lista de movimentos para o grid
        public IEnumerable<MovimentoManual> Movimentos { get; set; }

        // Objeto para o formulário de criação/edição
        public MovimentoManual MovimentoAtual { get; set; } = new MovimentoManual();
    }
}