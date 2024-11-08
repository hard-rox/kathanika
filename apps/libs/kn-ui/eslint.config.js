const nx = require('@nx/eslint-plugin');
const baseConfig = require('../../../eslint.base.config.js');

module.exports = [
    ...baseConfig,
    {
        files: ['**/*.json'],
        rules: {
            '@nx/dependency-checks': [
                'error',
                { ignoredFiles: ['{projectRoot}/eslint.config.{js,cjs,mjs}'] },
            ],
        },
        languageOptions: { parser: require('jsonc-eslint-parser') },
    },
    ...nx.configs['flat/angular'],
    ...nx.configs['flat/angular-template'],
    {
        files: ['**/*.ts'],
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
        },
    },
    {
        files: ['**/*.html'],
        // Override or add rules here
        rules: {},
    },
];
