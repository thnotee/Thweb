/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Views/**/*.cshtml",
        "./Areas/**/Views/**/*.cshtml",
        "./Areas/**/Pages/*.cshtml",
        "./Areas/**/Pages/**/*.cshtml",
    ],
  theme: {
    extend: {},
  },
  plugins: [],
}

//npx tailwindcss -i ./wwwroot/css/tailwind/input_tailwind.css -o ./wwwroot/css/app.css