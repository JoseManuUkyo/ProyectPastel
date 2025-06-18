// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", () => {
            const tabs = document.querySelectorAll('[role="tab"]');
            const panels = document.querySelectorAll('[role="tabpanel"]');

            // Oculta todos los paneles al inicio
            panels.forEach(panel => panel.style.display = "none");

            // Muestra el primero por defecto
            document.querySelector("#pes1").style.display = "block";

            tabs.forEach(tab => {
                tab.addEventListener("click", () => {
                    const targetPanel = document.getElementById(tab.getAttribute("aria-controls"));

                    // Oculta todos los paneles
                    panels.forEach(panel => panel.style.display = "none");

                    // Muestra el panel correspondiente
                    targetPanel.style.display = "block";
                });
            });
});

window.addEventListener("DOMContentLoaded", function () {
    const contenedor = document.getElementById("delice");

    if (contenedor) {
        const img = document.createElement("img");
        img.src = "/imagen/Prim_delice.JPG"; // Ruta relativa a tu carpeta de imágenes
        img.alt = "logo de Prim Delice";
        img.classList.add("img-delice");
        contenedor.appendChild(img);
    }
});

