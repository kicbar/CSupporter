const PROXY_CONFIG = [
  {
    context: [
      "/Product",
    ],
    target: "https://localhost:7048",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
