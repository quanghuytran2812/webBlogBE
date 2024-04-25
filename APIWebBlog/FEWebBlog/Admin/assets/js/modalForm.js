const btnOpenModal = document.getElementById("modalOpenOrderS");
const btnCloseModal = document.getElementById("modalCloseOrderS");

if (btnOpenModal) {
    btnOpenModal.onclick = () => {
        document.getElementById("modalEditSale").classList.add("showModal");
    };
}
if (btnCloseModal) {
    btnCloseModal.onclick = () => {
        document.getElementById("modalEditSale").classList.remove("showModal");
    };
}