window.addEventListener("DOMContentLoaded", function () {
    window.addEventListener("resize", getHeight);
    window.addEventListener("load", getHeight);

    function getHeight() {
        let height_nav_1 = document.querySelector("#nav-1").offsetHeight;
        let height_nav_2 = document.querySelector("#nav-2").offsetHeight;
        let height_footer = document.querySelector(".footer").offsetHeight;
        let final_height = height_nav_1 + height_nav_2;
        
        let finalHeight = `calc(100vh - ${final_height}px)`;
        let finalHeight2 = `calc(100vh - ${height_footer}px)`;
        
        document.querySelector(".full-image-1").style.height = finalHeight;
        document.querySelector(".full-image-2").style.height = finalHeight2;
        document.querySelector(".about-us").style.height = finalHeight;
        
    }

    getHeight(); // Po załadowaniu strony
});
