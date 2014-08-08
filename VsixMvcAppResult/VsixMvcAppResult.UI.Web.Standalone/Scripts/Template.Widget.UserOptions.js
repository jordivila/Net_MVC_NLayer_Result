﻿/// <reference path="VsixMvcAppResult.A.Intellisense.js" />

jQuery.widget("ui.userOptions", jQuery.ui.widgetBase,
{
    options: {

    },
    _create: function () {
        jQuery.ui.widgetBase.prototype._create.call(this);
    },
    _init: function () {

        jQuery.ui.widgetBase.prototype._init.call(this);

        var self = this;

        VsixMvcAppResult.Ajax.UserUpdateLastActivity(
                            function (data, textStatus, jqXHR) {
                                jQuery(self.element).append(data);
                                VsixMvcAppResult.Widgets.jQueryzer(self.element);
                            }
                            , function (jqXHR, textStatus, errorThrown) {

                                jQuery(self.element)
                                    .append("<div></div><div class='ui-carriageReturn'></div>")
                                    .find("div:first")
                                    .html(VsixMvcAppResult.Resources.unExpectedError);

                                VsixMvcAppResult.Widgets.DialogInline.Create(jQuery(self.element).find("div:first"), VsixMvcAppResult.Widgets.DialogInline.MsgTypes.Error);

                                jQuery(self.element)
                                    .find("div.ui-message div:first")
                                    .removeClass("ui-corner-all")
                                    .addClass("ui-corner-right");
                            }
                            , function () {
                                self._trigger('complete', null, null);
                            });
    },
    destroy: function () {
        jQuery.ui.widgetBase.prototype.destroy.call(this);
    }
});