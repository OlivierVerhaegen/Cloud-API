<template>
  <div class="bodyparts">
    <h4>
      <b>Add a Body part</b>
    </h4>

    <div>
      <vs-input class="margin-bottom" v-model="bpName" placeholder="Name">
        <template #icon>
          <i class='bx bxs-rename'></i>
        </template>
      </vs-input>
      <vs-input class="margin-bottom" v-model="bpLocation" placeholder="Location">
        <template #icon>
          <i class='bx bx-current-location'></i>
        </template>
      </vs-input>
      <vs-input class="margin-bottom" type="url" v-model="bpImageUrl" placeholder="Image url">
        <template #icon>
          <i class='bx bxs-image-add' ></i>
        </template>
      </vs-input>
      <vs-select
        class="margin-bottom"
        multiple
        filter
        placeholder="Select body parts"
        v-model="bpExercises"
        v-if="exercises.length > 0"
      >
        <vs-option v-for="e in exercises" :key="e.id" :label="e.name" :value="e">
          {{ e.name }}
        </vs-option>
      </vs-select>

      <vs-button style="margin: 0;" @click="addBodyPart()">
        Add Body part
      </vs-button>
    </div>

    <hr>
  </div>
</template>

<script>

export default {
  name: 'MyBodyParts',
  data:() => ({
    bpName: "",
    bpLocation: "",
    bpImageUrl: "",
    bpExercises: [],
    exercises: []
  }),
  methods: {
    openNotification(position = null, color, title, message) {
      const noti = this.$vs.notification({
        progress: 'auto',
        color,
        position,
        title: title,
        text: message
      })
    },
    async addBodyPart() {
      let exercisesIds = [];
      if (this.bpExercises != null) {
        this.bpExercises.forEach(e => {
          exercisesIds.push(
            {
              exerciseId: e.id
            }
          )
        });
      }
      
      let res = await fetch("https://" + window.location.hostname + ":44369/api/v1/bodyparts",
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          name: this.bpName,
          location: this.bpLocation,
          imageUrl: this.bpImageUrl,
          exercises: exercisesIds
        }),
      });
      
      if (res.ok) {
        this.openNotification(
          'top-right',
          'success',
          'Body part added',
          'The body part was added to the database.');
      } else {
        this.openNotification(
          'top-right',
          'danger',
          'Failed to add body part',
          'Name, location are required and image url should be valid.');
      }
    },
    getExercises() {
      fetch(
        "https://" + window.location.hostname + ":44369/api/v1/exercises",
        ).then((res) => {
          return res.json()
      }).then((data) => {
        if (data != null) {
          data.forEach(e => {
            this.exercises.push(e);
          });
        }
      })
    },
  },
  mounted() {
    this.getExercises();
  }
}
</script>

<style lang="scss" scoped>
.bodypart {
  padding: 2rem;
  border-radius: 2rem;

  &:nth-child(odd) {
    background-color: #f5f5f5;
  }

  .vs-button {
    margin: 0;
    margin-top: 1rem;
  }

  .delete {
    background-color: red;
  }
}
</style>