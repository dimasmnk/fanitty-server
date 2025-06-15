/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      keyframes: {
        fadeIn: {
          '0%': { opacity: 0 },
          '100%': { opacity: 1 }
        },
      },
      animation: {
        fadeIn: 'fadeIn ease-in-out 0.5s forwards',
      },
    },
  },
  daisyui: {
    themes: [
      {
        mytheme: {
          "primary": "#641ae6",
          "secondary": "#d926a9",
          "accent": "#1fb2a6",
          "neutral": "#191919",
          "base-100": "#000000",
          "info": "#3abff8",
          "success": "#36d399",
          "warning": "#fbbd23",
          "error": "#f87272",
        },
      },
    ],
  },
  plugins: [require("@tailwindcss/typography"), require("daisyui")],
}

