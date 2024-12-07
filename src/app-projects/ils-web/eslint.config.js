// @ts-check
const tseslint = require("typescript-eslint");
const rootConfig = require("../../../eslint.config.js");

module.exports = tseslint.config(
    ...rootConfig,
    {
        ignores:[
            "src/app-projects/ils-web/src/app/graphql/generated/graphql-operations.ts",
            "src/app-projects/ils-web/src/app/graphql/gql-test-utils.ts",
        ]
    },
    {
        files: [
            "**/*.ts"
        ],
        rules: {
            "@angular-eslint/directive-selector": [
                "error",
                {
                    type: "attribute",
                    prefix: "app",
                    style: "camelCase",
                },
            ],
            "@angular-eslint/component-selector": [
                "error",
                {
                    type: "element",
                    prefix: "app",
                    style: "kebab-case",
                },
            ],
        },
    },
    {
        files: ["**/*.html"],
        rules: {},
    }
);
