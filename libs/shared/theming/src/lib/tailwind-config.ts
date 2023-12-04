import { createGlobPatternsForDependencies } from '@nx/angular/tailwind';
import { join } from 'path';

export const content = [
  join(__dirname, 'src/**/!(*.stories|*.spec).{ts,html}'),
  ...createGlobPatternsForDependencies(__dirname),
]
export const tailwindConfig = {
  content: [...content],
  theme: {
    extend: {
      colors: {
        theme: {
          //https://coolors.co/b9baba-53585a-2a383d-00171f-0a8218-cb2929-e46e14
          'rich-black': '#00171f',
          'office-green': '#0A8218',
          'davys-gray': '#53585A',
          'fire-red': '#CB2929',
          'spanish-orange': '#E46E14',
          gunmetal: '#2a383d',
          silver: '#B9BABA', //Light
        },
      },
    },
  },
  plugins: [],
};
