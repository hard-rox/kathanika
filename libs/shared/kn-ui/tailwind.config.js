const { tailwindConfig, content } = require('../theming/src/index');

/** @type {import('tailwindcss').Config} */
module.exports = {
  presets: [tailwindConfig],
  content: [...content],
  theme: {},
  plugins: [],
};
