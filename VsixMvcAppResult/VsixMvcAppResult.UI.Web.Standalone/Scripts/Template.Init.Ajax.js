﻿


// This file has been auto generated by T4 Text Templates
// Changes to this file may be overriden at compile time






jQuery(document).ready(function () {
    jQuery.ajaxSetup({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
});

VsixMvcAppResult.Ajax = {};

VsixMvcAppResult.Ajax.ThemeSet = function (theme, onOK, onKO) {
    var jqxhr = jQuery.ajax({
        url: "/Theme/Set",
        type: "POST",
        data: JSON.stringify({ theme: theme })
    })
    .done(function (data, textStatus, jqXHR) {
        onOK(data);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        onKO(jqXHR);
    });
};
VsixMvcAppResult.Ajax.CultureSet = function (culture, onOK, onKO) {
    var jqxhr = jQuery.ajax({
        url: "/Culture/Set",
        type: "POST",
        data: JSON.stringify({ culture: culture })
    })
    .done(function (data, textStatus, jqXHR) {
        onOK(data);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        onKO(jqXHR);
    });
};
VsixMvcAppResult.Ajax.UserBar = function (onOK, onKO, onComplete) {
    var jqxhr = jQuery.ajax({
			url: "/UserAccountBar/UserAccountBar"
            , type: "GET"
            , data: {}
            , dataType: "html"
            , cache: false
    })
    .done(function (data, textStatus, jqXHR) {
        onOK(data);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        onKO(jqXHR);
    })
	.always(function (jqXHR, textStatus, errorThrown) {
		onComplete();
	});
};

