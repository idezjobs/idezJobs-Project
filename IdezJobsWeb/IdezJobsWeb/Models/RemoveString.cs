using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdezJobsWeb.Models
{
	public static class RemoveString
	{
		public static string GenerateKeyWords(string text)
		{
			// converter toda o texto para minúsculas
			text = text.ToLower( );

			// remover toda a pontuação (ou substituir por espaço)
			text = new string(text.Where(c => !char.IsPunctuation(c)).ToArray( ));

			string[] KeyStrings = {"a","ante","após","até","com","conforme","contra","consoante","de","desde", "durante","em",
			                       "entre","mediante ",	"para","perante","por","salvo","sem","segundo","sob","sobre", "trás", "o",
			                       "à","aos","às","de","do","da","dos", "das","dum","duma","duns","dumas",	"em", "no",	"na", "nos",
			                       "num","numa","nuns", "numas","por","pelo","pela","pelos", "pelas","excepto","ao","nas","os",
								   "uma","um", "uns", "umas", "que", "ou", "seu", "se","é"};

			string[] palavras = text.Split(new char[] { ' ' });
			List<String> novas = new List<string>();
			bool adiciona;

			// remove palavras desnecessárias
			foreach (var palavra in palavras)
			{
				adiciona = true;
				foreach (var itemKeyStrings in KeyStrings)
				{
					if (palavra == itemKeyStrings) {
						adiciona = false;
					}
				}
				if (adiciona)
					novas.Add(palavra);
			}

			// remover as palavras repetidas
			//novas.Distinct( );

			text = String.Join(",", novas.Distinct());
			
			return text;
		}

	}
}
