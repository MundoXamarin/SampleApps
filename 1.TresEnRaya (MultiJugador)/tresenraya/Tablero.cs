using System;
using System.Linq;

namespace enRaya
{
	enum ValorCelda : byte
	{
		Vacio,
		JugadorX,
		JugadorO

	}



	class Tablero
	{
		public ValorCelda[] m_Valores;
		bool m_TurnoJugadorX;
		public bool JuegoTerminado { get; set; }
		public ValorCelda m_Ganador;

		public Tablero(ValorCelda[] values, bool turnForPlayerX)
		{
			m_TurnoJugadorX = turnForPlayerX;
			m_Valores = values;
			ScoreGame();

		}


		public void ScoreGame()
		{
			int[,] jugadasGanadoras = { 
							 { 0, 1, 2 },
							 { 3, 4, 5 },
							 { 6, 7, 8 },
							 { 0, 3, 6 },
							 { 1, 4, 7 },
							 { 2, 5, 8 },
							 { 0, 4, 8 },
							 { 2, 4, 6 }
						   };
			int countX = 0;
			int countO = 0;
			for (int i = jugadasGanadoras.GetLowerBound(0); i <= jugadasGanadoras.GetUpperBound(0); i++)
			{
				var one = m_Valores[jugadasGanadoras[i, 0]];
				var two = m_Valores[jugadasGanadoras[i, 1]];
				var three = m_Valores[jugadasGanadoras[i, 2]];
				var fila = new ValorCelda[] { one, two, three };
				countX = fila.Count(x => x == ValorCelda.JugadorX);
				countO = fila.Count(y => y == ValorCelda.JugadorO);
				if (countO == 3 || countX == 3)
				{
					JuegoTerminado = true;
					break;
				}
			}

			m_Ganador = countX == 3 ? ValorCelda.JugadorX : countO == 3 ? ValorCelda.JugadorO : ValorCelda.Vacio;

		}



		public bool EsNodoTerminal()
		{
			if (JuegoTerminado)
				return true;
			
			foreach (ValorCelda v in m_Valores)
			{
				if (v == ValorCelda.Vacio)
					return false;
			}
			return true;
		}

		public Tablero ObtenerNuevoTableroPos(int indexJugada)
		{
			ValorCelda[] nuevosValores = (ValorCelda[])m_Valores.Clone();

			if (m_Valores[indexJugada] != ValorCelda.Vacio)
				throw new Exception(string.Format("Posicion [{0}] tomada por {1}", indexJugada, m_Valores[indexJugada]));


			nuevosValores[indexJugada] = m_TurnoJugadorX ? ValorCelda.JugadorX : ValorCelda.JugadorO;
			return new Tablero(nuevosValores, !m_TurnoJugadorX);
		}



	}

	class TresEnRaya
	{
		public Tablero TableroActual { get; set; }


		public TresEnRaya()
		{
			ValorCelda[] valores = Enumerable.Repeat(ValorCelda.Vacio, 9).ToArray();
			TableroActual = new Tablero(valores, true);
		}



		public void HacerMovUsuario(int pos)
		{
			if (TableroActual.EsNodoTerminal())
				return;

			try
			{
				TableroActual = TableroActual.ObtenerNuevoTableroPos(pos);
				return;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				throw e;

			}

		}

	}
}
