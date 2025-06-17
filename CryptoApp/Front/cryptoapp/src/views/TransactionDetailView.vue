<template>
  <div>
    <h1>Detalle de Transacción</h1>

    <div v-if="!isEditMode">
      <p><strong>Criptomoneda:</strong> {{ transaction.cryptoCode }}</p>
      <p><strong>Acción:</strong> {{ transaction.action }}</p>
      <p><strong>Cantidad:</strong> {{ transaction.cryptoAmount }}</p>
      <p><strong>Monto (ARS):</strong> {{ transaction.money }}</p>
      <p><strong>Fecha:</strong> {{ new Date(transaction.dateTime).toLocaleString() }}</p>
      <button @click="goToEditMode">Editar</button>
      <button @click="goBack">Volver</button>
    </div>

    <div v-else>
      <form @submit.prevent="updateTransaction">
        <label>Monto (ARS):</label>
        <input type="number" v-model="transaction.money" min="0" required />
        <button type="submit">Guardar</button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '../services/api'

const router = useRouter()
const route = useRoute()
const transaction = ref({})

const isEditMode = computed(() => route.query.edit === 'true')

onMounted(async () => {
  try {
    const res = await api.get(`/transactions/${route.params.id}`) // Trae los datos de la transacción
    transaction.value = res.data
  } catch (error) {
    console.error('Error al cargar la transacción:', error)
    alert('Error al cargar la transacción. Ver consola.')
  }
})

function goToEditMode() {
  router.push(`/transactions/${route.params.id}?edit=true`)
}

function goBack() {
  router.push('/transactions')
}

async function updateTransaction() {
  try {
    await api.patch(`/transactions/${route.params.id}`, { money: transaction.value.money }) // 📌 PATCH correcto
    alert('Monto actualizado correctamente.') // 📌 Alerta de éxito
    router.push('/transactions') // 📌 Redirige al historial
  } catch (error) {
    console.error('Error al actualizar transacción:', error) // 📌 Muestra el error en consola si falla
    alert('Error al actualizar transacción. Ver consola.')
  }
}
</script>
