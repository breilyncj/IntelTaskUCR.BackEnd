﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntelTaskUCR.Domain.Entities
{
    public class EAdjuntosXTareas
    {
        public int CN_Id_adjuntos { get; set; }

        public int CN_Id_tarea { get; set; }

        public EAdjuntos Adjunto { get; set; }
        public ETareas Tarea { get; set; }
    }
}
