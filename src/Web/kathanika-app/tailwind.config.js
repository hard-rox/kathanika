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
          'light-blue': '#c0e9f8ff',
          'sky-blue': '#80d3f0ff',
          'picton-blue': '#00a7e1ff',
          'rich-black': '#00171fff',
          'prussian-blue': '#00263cff',
          'indigo-dye': '#005272',
          'cerulean': '#007ea7ff'
        }
      }
    },
  },
  plugins: [],
}

