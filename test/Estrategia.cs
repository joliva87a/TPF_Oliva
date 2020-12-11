
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{


		public String Consulta1(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> c = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			int contNivel = 0;
			string mensaje = "";
			c.encolar(arbol);
			c.encolar(null);
			while (!c.esVacia())
			{
				arbolAux = c.desencolar();

				if (arbolAux == null)
				{

					if (!c.esVacia())
					{
						c.encolar(null);
						contNivel++;
					}
				}

				else
				{

					if (arbolAux.getDatoRaiz().EsPlanetaDeLaIA())
					{
						mensaje = "La distancia entre la raiz y el nodo del bot es " + contNivel;
						break;
					}
					foreach (var hijo in arbolAux.getHijos())
					{
						c.encolar(hijo);

					}
				}

			}

			return mensaje;
		}


		public String Consulta2(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> q = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			q.encolar(arbol);
			int nivel = 0;
			String mensaje = "";
			while (!q.esVacia())
			{
				int elementos = q.cantElementos();
				nivel++;
				int cantidadPorNivel = 0;
				while (elementos-- > 0)
				{
					arbolAux = q.desencolar();

					if (arbolAux.getDatoRaiz().Poblacion() > 10)
					{
						cantidadPorNivel++;
					}

					foreach (ArbolGeneral<Planeta> hijo in arbolAux.getHijos())
					{
						q.encolar(hijo);
					}
				}
				mensaje += "Nivel " + nivel + ": " + cantidadPorNivel + ", ";
			}
			return mensaje;
		}

		public String Consulta3(ArbolGeneral<Planeta> arbol)
		{
			Cola<ArbolGeneral<Planeta>> q = new Cola<ArbolGeneral<Planeta>>();
			ArbolGeneral<Planeta> arbolAux;
			q.encolar(arbol);
			int nivel = 0;
			String mensaje = "";
			while (!q.esVacia())
			{
				int elementos = q.cantElementos();
				nivel++;
				int cantidadPorNivel = 0;
				int poblacionPorNivel = 0;
				while (elementos-- > 0)
				{
					arbolAux = q.desencolar();

					cantidadPorNivel++;
					poblacionPorNivel += arbolAux.getDatoRaiz().Poblacion();

					foreach (ArbolGeneral<Planeta> hijo in arbolAux.getHijos())
					{
						q.encolar(hijo);
					}
				}
				mensaje += "Nivel " + nivel + ": " + poblacionPorNivel / cantidadPorNivel + "\n";
			}
			return mensaje;
		}

		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{

			if (!arbol.getDatoRaiz().EsPlanetaDeLaIA())//Si la raiz no es de la IA
			{
				List<Planeta> caminito = this.caminoConPreordenIA(arbol);
				int i = caminito.Count;
				Movimiento ataque = new Movimiento(caminito[i - 1], caminito[i - 2]);
				return ataque;
			}
			else //Si la raiz es de la IA
			{
				List<Planeta> caminito = this.caminoConPreordenPlayer(arbol);
				int i;
				for (i = 0; i < caminito.Count; i++)
				{
					if (!caminito[i].EsPlanetaDeLaIA())
					{
						i--;
						break;
					}
				}
				Movimiento ataque = new Movimiento(caminito[i], caminito[i + 1]);
				return ataque;
			}


		}

		private List<Planeta> _caminoConPreordenIA(ArbolGeneral<Planeta> arbol, List<Planeta> camino)
		{
			//Primero la raiz
			camino.Add(arbol.getDatoRaiz());

			//si encontramos camino...
			if (arbol.getDatoRaiz().EsPlanetaDeLaIA())
			{
				return camino;
			}
			else
			{
				//Luego hijos recursivamente
				foreach (var hijo in arbol.getHijos())
				{
					List<Planeta> caminoAux = _caminoConPreordenIA(hijo, camino);
					if (caminoAux != null)
					{
						return caminoAux;
					}

					//saco ultimo planeta del camino
					camino.RemoveAt(camino.Count - 1);
				}
			}
			return null;
		}


		public List<Planeta> caminoConPreordenIA(ArbolGeneral<Planeta> arbol)
		{
			List<Planeta> camino = new List<Planeta>();
			return _caminoConPreordenIA(arbol, camino);
		}


		private List<Planeta> _caminoConPreordenPlayer(ArbolGeneral<Planeta> arbol, List<Planeta> camino)
		{
			//Primero la raiz
			camino.Add(arbol.getDatoRaiz());

			//si encontramos camino...
			if (arbol.getDatoRaiz().EsPlanetaDelJugador())
			{
				return camino;
			}
			else
			{
				//Luego hijos recursivamente
				foreach (var hijo in arbol.getHijos())
				{
					List<Planeta> caminoAux = _caminoConPreordenPlayer(hijo, camino);
					if (caminoAux != null)
					{
						return caminoAux;
					}

					//saco ultimo planeta del camino
					camino.RemoveAt(camino.Count - 1);
				}
			}
			return null;
		}

		public List<Planeta> caminoConPreordenPlayer(ArbolGeneral<Planeta> arbol)
		{
			List<Planeta> camino = new List<Planeta>();
			return _caminoConPreordenPlayer(arbol, camino);
		}



		/*
		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			//Implementar
			
			return null;
		}*/
	}
}
