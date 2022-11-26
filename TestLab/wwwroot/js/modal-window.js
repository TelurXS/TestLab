
const modal_window = $("[modal-window]");
const modal_open = $("[modal-open]");
const modal_close = $("[modal-close]");

var on_modal_open = function (data) { };
var on_modal_close = function () { };

const ModalWindowOpen = (e) => {
    modal_window.removeClass("modal-closed");
    var data = $(e.target).attr("modal-data")
    on_modal_open(data);
}

const ModalWindowClose = () => {
    modal_window.addClass("modal-closed");
    on_modal_close();
}

modal_open.click(ModalWindowOpen);
modal_close.click(ModalWindowClose);