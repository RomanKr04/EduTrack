window.showAlert = function (message) {
    alert(message);
};

window.focusOnInput = function (elementId) {
    var element = document.getElementById(elementId);
    if (element) {
        element.focus();
    }
};
// Пример неверного кода, который может привести к рекурсии:


function test(dsfdf) {

}
/*window.showToast = () => {
    const toast = new bootstrap.Toast(document.getElementById('alert-toast'));
    toast.show();
};*/
document.addEventListener("DOMContentLoaded", () => {
    window.showAlert = (message) => {
        const toast = document.createElement("div");
        toast.className = "toast";
        toast.textContent = message;
        document.body.appendChild(toast);

        setTimeout(() => {
            toast.classList.add("show");
        }, 100);

        setTimeout(() => {
            toast.classList.remove("show");
            toast.addEventListener("transitionend", () => toast.remove());
        }, 3000);
    };
    window.goBack = function () {
        window.history.back();
    };


    window.showToast = () => {
        alert("Custom toast function triggered!");
    };
});

