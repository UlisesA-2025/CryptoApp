<template>
  <div>
    <h1>Crypto Wallet</h1>
    <nav>
      <router-link to="/transactions">📋 Historial de Transacciones</router-link> |
      <router-link to="/transactions/new">📝 Nueva Transacción</router-link> |
      <router-link to="/portfolio">💰 Ver Portafolio</router-link>
    </nav>
  </div>
</template>

<script setup>
// Nada por ahora
</script>

<style scoped>
nav {
  margin-top: 20px;
}
nav a {
  margin-right: 15px;
}
</style>
