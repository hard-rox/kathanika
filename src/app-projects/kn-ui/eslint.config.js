// @ts-check
const tseslint = require("typescript-eslint");
const rootConfig = require("../../../eslint.config.js");

module.exports = tseslint.config(
    ...rootConfig,
    {
        files: ["**/*.ts"],
        rules: {
            '@angular-eslint/directive-selector': [
                'error',
                {
                    type: 'attribute',
                    prefix: 'kn',
                    style: 'kebab-case',
                },
            ],
            '@angular-eslint/component-selector': [
                'error',
                {
                    type: 'element',
                    prefix: 'kn',
                    style: 'kebab-case',
                },
            ],
            '@angular-eslint/directive-class-suffix': [
                'error',
                {
                    suffixes: [''],
                },
            ],
            '@angular-eslint/component-class-suffix': [
                'error',
                {
                    suffixes: [''],
                },
            ],
        }
    },
    {
        files: ["**/*.html"],
        rules: {},
    }
);
