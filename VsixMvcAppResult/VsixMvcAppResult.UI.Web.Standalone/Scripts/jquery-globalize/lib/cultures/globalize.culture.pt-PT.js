/*
 * Globalize Culture pt-PT
 *
 * http://github.com/jquery/globalize
 *
 * Copyright Software Freedom Conservancy, Inc.
 * Dual licensed under the MIT or GPL Version 2 licenses.
 * http://jquery.org/license
 *
 * This file was generated by the Globalize Culture Generator
 * Translation: bugs found in this file need to be fixed in the generator
 */

(function( window, undefined ) {

var Globalize;

if ( typeof require !== "undefined"
	&& typeof exports !== "undefined"
	&& typeof module !== "undefined" ) {
	// Assume CommonJS
	Globalize = require( "globalize" );
} else {
	// Global variable
	Globalize = window.Globalize;
}

Globalize.addCultureInfo( "pt-PT", "default", {
	name: "pt-PT",
	englishName: "Portuguese (Portugal)",
	nativeName: "português (Portugal)",
	language: "pt",
	numberFormat: {
		",": ".",
		".": ",",
		NaN: "NaN (Não é um número)",
		negativeInfinity: "-Infinito",
		positiveInfinity: "+Infinito",
		percent: {
			pattern: ["-n%","n%"],
			",": ".",
			".": ","
		},
		currency: {
			pattern: ["-n $","n $"],
			",": ".",
			".": ",",
			symbol: "€"
		}
	},
	calendars: {
		standard: {
			"/": "-",
			firstDay: 1,
			days: {
				names: ["domingo","segunda-feira","terça-feira","quarta-feira","quinta-feira","sexta-feira","sábado"],
				namesAbbr: ["dom","seg","ter","qua","qui","sex","sáb"],
				namesShort: ["D","S","T","Q","Q","S","S"]
			},
			months: {
				names: ["Janeiro","Fevereiro","Março","Abril","Maio","Junho","Julho","Agosto","Setembro","Outubro","Novembro","Dezembro",""],
				namesAbbr: ["Jan","Fev","Mar","Abr","Mai","Jun","Jul","Ago","Set","Out","Nov","Dez",""]
			},
			AM: null,
			PM: null,
			eras: [{"name":"d.C.","start":null,"offset":0}],
			patterns: {
				d: "dd-MM-yyyy",
				D: "dddd, d' de 'MMMM' de 'yyyy",
				t: "HH:mm",
				T: "HH:mm:ss",
				f: "dddd, d' de 'MMMM' de 'yyyy HH:mm",
				F: "dddd, d' de 'MMMM' de 'yyyy HH:mm:ss",
				M: "d/M",
				Y: "MMMM' de 'yyyy"
			}
		}
	}
});

}( this ));
