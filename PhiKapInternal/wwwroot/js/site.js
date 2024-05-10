// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("click", e => {
    console.log('dropping')
    const isDropdownButton = e.target.matches("[data-dropdown-button]")
    if (!isDropdownButton && e.target.closest("[data-dropdown]") != null) return

    let currentDropdown
    if (isDropdownButton) {
        currentDropdown = e.target.closest("[data-dropdown]")
        currentDropdown.classList.toggle("active")
    }
    console.log(currentDropdown)
    document.querySelectorAll("[data-dropdown].active").forEach(dropdown => {
        console.log(dropdown)
        if (dropdown === currentDropdown) return
        dropdown.classList.remove("active")
    })
})