<template>
  <div>
    <h1>Nueva Transacción</h1>
    <form @submit.prevent="createTransaction">
      <label>Criptomoneda:</label>
      <select v-model="transaction.cryptoCode" required>
        <option value="usdt">Tether (USDT)</option>
        <option value="usdc">USD Coin (USDC)</option>
        <option value="dai">DAI</option>
        <option value="btc">Bitcoin (BTC)</option>
        <option value="eth">Ethereum (ETH)</option>
      </select>

      <label>Acción:</label>
      <select v-model="transaction.action" required>
        <option value="purchase">Compra</option>
        <option value="sale">Venta</option>
      </select>

      <label>Cantidad:</label>
      <input type="number" v-model="transaction.cryptoAmount" min="0.0001" step="0.0001" required />

      <label>Fecha:</label>
      <input type="datetime-local" v-model="transaction.dateTime" required />

      <button type="submit">Guardar</button>
    </form>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import api from '../services/api'

// 📌 Variable para movernos entre rutas
const router = useRouter()

// 📌 Datos del formulario
const transaction = ref({
  cryptoCode: 'usdc',
  action: 'purchase',
  cryptoAmount: 0,
  dateTime: ''
})

// 📌 Método para enviar al backend
async function createTransaction() {
  try {

    console.log(transaction.value)

    // Llama al backend (usa tu endpoint POST)
    await api.post('/transactions', transaction.value)

    // Muestra mensaje de éxito
    alert('Transacción guardada correctamente.')

    // Redirige al historial
    router.push('/transactions')
  } catch (error) {
    console.error('Error al guardar transacción:', error)
    alert('Ocurrió un error al guardar. Ver consola.')
  }
}
</script>
