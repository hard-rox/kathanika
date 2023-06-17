/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
    "index.html",
  ],
  theme: {
    extend: {
      colors: {
        theme: {
          //https://coolors.co/b9baba-53585a-2a383d-00171f-0a8218-cb2929-e46e14
          'rich-black': '#00171f', //Dark
          'office-green': '#0A8218', //Success
          'davys-gray': '#53585A', //Secondary
          'fire-red': '#CB2929', //Danger/Error
          'spanish-orange': '#E46E14', //Warning
          'gunmetal': '#2a383d', //Info
          'silver': '#B9BABA', //Light
        }
      }
    },
  },
  plugins: [],
}

