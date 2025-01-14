﻿using Bot_Dofus_1._29._1.Otros.Mapas.Entidades;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot_Dofus_1._29._1.Otros.Scripts.Acciones.Npcs
{
    public class RespuestaAccion : AccionesScript
    {
        public short respuesta_id { get; private set; }

        public RespuestaAccion(short _respuesta_id) => respuesta_id = _respuesta_id;

        internal override Task<ResultadosAcciones> proceso(Account cuenta)
        {
            if (!cuenta.IsDialoguing())
                return resultado_fallado;

            IEnumerable<Otros.Mapas.Entidades.Npcs> npcs = cuenta.Game.Map.lista_npcs();
            Otros.Mapas.Entidades.Npcs npc = npcs.ElementAt((cuenta.Game.Character.hablando_npc_id * -1) - 1);

            if(npc == null)
                return resultado_fallado;

            if (respuesta_id < 0)
            {
                int index = (respuesta_id * -1) - 1;

                if (npc.respuestas.Count <= index)
                    return resultado_fallado;

                respuesta_id = npc.respuestas[index];
            }

            if (!npc.respuestas.Contains(respuesta_id))
                return resultado_fallado;

            cuenta.Connection.Send("DR" + npc.pregunta + "|" + respuesta_id, true);
            return resultado_procesado;
        }
    }
}
