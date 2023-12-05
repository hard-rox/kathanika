const { tailwindConfig } = require('../theming/src/index');
const { join } = require('path');

/** @type {import('tailwindcss').Config} */
module.exports = {
  presets: [tailwindConfig],
  content: [
    join(__dirname, 'src/**/!(*.stories|*.spec).{ts,html}')
  ],
  theme: {},
  plugins: [],
};
