/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Views/Shared/*.cshtml",
        "./Views/Shared/_Partial/*.cshtml",
        "./Areas/**/Views/**/*.cshtml",
        "./Areas/**/Pages/*.cshtml",
        "./Areas/**/Pages/**/*.cshtml",
        "./node_modules/flowbite/**/*.js",
    ],
  theme: {
    extend: {},
  },
    plugins: [
        require('flowbite/plugin')
    ],
}

//npx tailwindcss -i ./wwwroot/css/tailwind/input_tailwind.css -o ./wwwroot/css/app.css --watch
//npx tailwindcss -i ./wwwroot/css/tailwind/input_tailwind.css -o ./wwwroot/css/app.css