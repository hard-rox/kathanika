const { tailwindConfig } = require('../theming/src/index');
const { join } = require('path');

/** @type {import('tailwindcss').Config} */
module.exports = {
  presets: [tailwindConfig],
  content: [
    join(__dirname, 'src/**/!(*.spec).{ts,html}')
  ],
  theme: {},
  plugins: [],
};
