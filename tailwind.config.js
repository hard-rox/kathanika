const { knUiTwConfig } = require('./src/app-projects/kn-ui/src/tailwind.config');

/** @type {import('tailwindcss').Config} */
module.exports = {
  presets: [knUiTwConfig],
  content: [
    "./src/app-projects/**/*.{html,ts}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
