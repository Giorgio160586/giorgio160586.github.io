document.addEventListener('DOMContentLoaded', function () {
    const imageModal = document.getElementById('imageModal');
    if (imageModal) {
        imageModal.addEventListener('show.bs.modal', function (event) {
            const img = event.relatedTarget;
            const src = img.getAttribute('data-bs-img');
            const modalImage = document.getElementById('modalImage');
            if (modalImage) {
                modalImage.src = src;
            }
        });
    }
});
